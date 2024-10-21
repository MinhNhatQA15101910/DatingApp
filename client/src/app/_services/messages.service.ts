import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Message } from '../_models/message';
import { PaginatedResult } from '../_models/pagination';
import { setPaginatedResponse, setPaginationHeaders } from './paginationHelper';

@Injectable({
  providedIn: 'root',
})
export class MessagesService {
  private http = inject(HttpClient);

  baseUrl = environment.apiUrl;
  paginatedResult = signal<PaginatedResult<Message[]> | null>(null);

  getMessages(container: string, pageNumber: number, pageSize: number) {
    let params = setPaginationHeaders(pageNumber, pageSize);

    params = params.append('container', container);

    return this.http
      .get<Message[]>(`${this.baseUrl}messages`, {
        observe: 'response',
        params,
      })
      .subscribe({
        next: (response) =>
          setPaginatedResponse(response, this.paginatedResult),
      });
  }

  getMessageThread(username: string) {
    return this.http.get<Message[]>(
      `${this.baseUrl}messages/thread/${username}`
    );
  }
}
