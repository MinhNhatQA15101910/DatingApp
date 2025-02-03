import { Component, inject, OnInit } from '@angular/core';
import { AdminService } from '../../_services/admin.service';
import { Photo } from '../../_models/photo';

@Component({
  selector: 'app-photo-management',
  standalone: true,
  imports: [],
  templateUrl: './photo-management.component.html',
  styleUrl: './photo-management.component.css',
})
export class PhotoManagementComponent implements OnInit {
  private adminService = inject(AdminService);

  photos: Photo[] = [];

  approvePhoto(photoId: number) {
    this.adminService.approvePhoto(photoId).subscribe({
      next: (_) => (this.photos = this.photos.filter((x) => x.id !== photoId)),
    });
  }

  getPhotosForApproval() {
    this.adminService.getPhotosForApproval().subscribe({
      next: (photos) => (this.photos = photos),
    });
  }

  ngOnInit(): void {
    this.getPhotosForApproval();
  }

  rejectPhoto(photoId: number) {
    this.adminService.rejectPhoto(photoId).subscribe({
      next: (_) => (this.photos = this.photos.filter((x) => x.id !== photoId)),
    });
  }
}
