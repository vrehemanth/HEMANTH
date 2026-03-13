import { Injectable, inject, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

const API_BASE = environment.apiBase;
import { ToastService } from './toast.service';

export interface Notification {
    id: string;
    title: string;
    message: string;
    type: 'Info' | 'Success' | 'Warning' | 'Error';
    isRead: boolean;
    createdAt: string;
}

@Injectable({
    providedIn: 'root'
})
export class NotificationService {
    private http = inject(HttpClient);
    private toast = inject(ToastService);
    private apiUrl = `${API_BASE}/Notifications`;

    notifications = signal<Notification[]>([]);
    unreadCount = signal<number>(0);

    private pollInterval: any;

    constructor() { }

    startPolling() {
        if (this.pollInterval) return;
        this.fetchNotifications();
        this.fetchUnreadCount();
        this.pollInterval = setInterval(() => {
            this.fetchUnreadCount();
        }, 15000); // Check every 15 seconds
    }

    stopPolling() {
        if (this.pollInterval) {
            clearInterval(this.pollInterval);
            this.pollInterval = null;
        }
    }

    fetchNotifications(all = false) {
        this.http.get<any>(`${this.apiUrl}${all ? '?all=true' : ''}`).subscribe(res => {
            const data = res?.data || res;
            this.notifications.set(Array.isArray(data) ? data : (data?.$values || []));
        });
    }

    fetchUnreadCount() {
        this.http.get<any>(`${this.apiUrl}/unread-count`).subscribe(res => {
            const data = res?.data || res;
            const count = typeof data === 'number' ? data : (data?.count || 0);

            if (count > this.unreadCount()) {
                // Fetch notifications to get the latest message
                this.http.get<any>(`${this.apiUrl}?take=1`).subscribe(notifRes => {
                    const latestData = notifRes?.data || notifRes;
                    const list = Array.isArray(latestData) ? latestData : (latestData?.$values || []);
                    if (list.length > 0) {
                        const latest = list[0];
                        this.showNotificationToast(latest);
                    } else {
                        this.toast.info('You have new notifications');
                    }
                    this.fetchNotifications();
                });
            }
            this.unreadCount.set(count);
        });
    }

    private showNotificationToast(notification: Notification) {
        const type = notification.type.toLowerCase() as 'success' | 'error' | 'info' | 'warning';
        this.toast.show(notification.message, type);
    }

    markAsRead(id: string) {
        this.http.post(`${this.apiUrl}/${id}/read`, {}).subscribe(() => {
            this.notifications.update(list =>
                list.map(n => n.id === id ? { ...n, isRead: true } : n)
            );
            this.fetchUnreadCount();
        });
    }

    markAllAsRead() {
        this.http.post(`${this.apiUrl}/read-all`, {}).subscribe(() => {
            this.notifications.update(list => list.map(n => ({ ...n, isRead: true })));
            this.unreadCount.set(0);
        });
    }
}
