import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ToastService } from './core/services/toast.service';
import { ChatComponent } from './components/shared/chat/chat';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule, ChatComponent],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  toastService = inject(ToastService);
}
