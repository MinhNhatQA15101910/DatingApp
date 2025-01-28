import { Component, inject, input, ViewChild } from '@angular/core';
import { TimeagoModule } from 'ngx-timeago';
import { MessagesService } from '../../_services/messages.service';
import { FormsModule, NgForm } from '@angular/forms';

@Component({
  selector: 'app-member-messages',
  standalone: true,
  imports: [TimeagoModule, FormsModule],
  templateUrl: './member-messages.component.html',
  styleUrl: './member-messages.component.css',
})
export class MemberMessagesComponent {
  messagesService = inject(MessagesService);

  @ViewChild('messageForm') messageForm?: NgForm;
  username = input.required<string>();
  messageContent = '';

  sendMessage() {
    this.messagesService
      .sendMessage(this.username(), this.messageContent)
      .then(() => this.messageForm?.reset());
  }
}
