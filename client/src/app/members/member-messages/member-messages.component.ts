import {
  AfterViewChecked,
  Component,
  inject,
  input,
  ViewChild,
} from '@angular/core';
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
export class MemberMessagesComponent implements AfterViewChecked {
  messagesService = inject(MessagesService);

  @ViewChild('messageForm') messageForm?: NgForm;
  @ViewChild('scrollMe') scrollContainer?: any;
  username = input.required<string>();
  messageContent = '';

  ngAfterViewChecked(): void {
    this.scrollToBottom();
  }

  sendMessage() {
    this.messagesService
      .sendMessage(this.username(), this.messageContent)
      .then(() => this.messageForm?.reset());
    this.scrollToBottom();
  }

  private scrollToBottom() {
    this.scrollContainer.nativeElement.scrollTop =
      this.scrollContainer.nativeElement.scrollHeight;
  }
}
