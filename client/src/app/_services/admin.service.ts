import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { User } from '../_models/user';
import { Photo } from '../_models/photo';

@Injectable({
  providedIn: 'root',
})
export class AdminService {
  private http = inject(HttpClient);

  baseUrl = environment.apiUrl;

  approvePhoto(photoId: number) {
    return this.http.post(`${this.baseUrl}admin/approve-photo/${photoId}`, {});
  }

  getPhotosForApproval() {
    return this.http.get<Photo[]>(`${this.baseUrl}admin/photos-to-moderate`);
  }

  getUsersWithRoles() {
    return this.http.get<User[]>(`${this.baseUrl}admin/users-with-roles`);
  }

  rejectPhoto(photoId: number) {
    return this.http.post(`${this.baseUrl}admin/reject-photo/${photoId}`, {});
  }

  updateUserRoles(username: string, roles: string[]) {
    return this.http.post<string[]>(
      `${this.baseUrl}admin/edit-roles/${username}?roles=${roles}`,
      {}
    );
  }
}
