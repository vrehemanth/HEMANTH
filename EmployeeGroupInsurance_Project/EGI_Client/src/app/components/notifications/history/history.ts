import { Component, inject, signal, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotificationService, Notification } from '../../../core/services/notification.service';

@Component({
    selector: 'app-notification-history',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './history.html'
})
export class NotificationHistoryComponent implements OnInit, OnDestroy {
    notificationService = inject(NotificationService);

    ngOnInit() {
        // Fetch all history
        this.notificationService.fetchNotifications(true);
    }

    ngOnDestroy() {
        // Reset to limited view when leaving
        this.notificationService.fetchNotifications(false);
    }
}
