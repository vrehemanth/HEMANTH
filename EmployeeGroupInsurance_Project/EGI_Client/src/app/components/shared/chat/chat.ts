import { Component, ElementRef, ViewChild, inject, signal, effect } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';

interface ChatMessage {
  role: 'user' | 'assistant';
  content: string;
}

interface ApiResponse<T> {
  success: boolean;
  message: string;
  data: T;
}

@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './chat.html',
  styleUrl: './chat.css'
})
export class ChatComponent {
  private http = inject(HttpClient);
  
  isOpen = signal<boolean>(false);
  isTyping = signal<boolean>(false);
  inputMessage = signal<string>('');
  messages = signal<ChatMessage[]>([
    { role: 'assistant', content: 'Hello! I am your Employee Group Insurance App Assistant. How can I help you today?' }
  ]);

  @ViewChild('scrollMe') private myScrollContainer!: ElementRef;

  constructor() {
    effect(() => {
      // scroll to bottom whenever messages change
      if (this.messages().length > 0 && this.isOpen()) {
        setTimeout(() => this.scrollToBottom(), 100);
      }
    });
  }

  toggleChat() {
    this.isOpen.set(!this.isOpen());
    if (this.isOpen()) {
      setTimeout(() => this.scrollToBottom(), 100);
    }
  }

  sendMessage() {
    const msg = this.inputMessage().trim();
    if (!msg) return;

    // Add user message to UI
    this.messages.update(m => [...m, { role: 'user', content: msg }]);
    this.inputMessage.set('');
    this.isTyping.set(true);

    const apiUrl = `${environment.apiBase}/chat`;
    
    this.http.post<ApiResponse<{response: string}>>(apiUrl, { message: msg }).subscribe({
      next: (res) => {
        const aiResponse = res?.data?.response || 'No response';
        this.messages.update(m => [...m, { role: 'assistant', content: aiResponse }]);
        this.isTyping.set(false);
      },
      error: (err) => {
        console.error('Chat error', err);
        this.messages.update(m => [...m, { role: 'assistant', content: 'Sorry, I am having trouble connecting to the server. Please try again later.' }]);
        this.isTyping.set(false);
      }
    });
  }

  handleKeyDown(event: KeyboardEvent) {
    if (event.key === 'Enter' && !event.shiftKey) {
      event.preventDefault();
      this.sendMessage();
    }
  }

  private scrollToBottom(): void {
    try {
      this.myScrollContainer.nativeElement.scrollTop = this.myScrollContainer.nativeElement.scrollHeight;
    } catch(err) { }
  }
}
