import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

import { Vendor } from '../models/vendor.model';
import { VendorService } from '../services/vendor.service';

@Component({
  selector: 'app-vendor-list',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './vendor-list.component.html',
  styleUrl: './vendor-list.component.scss'
})
export class VendorListComponent implements OnInit {
  vendors: Vendor[] = [];
  skip = 0;
  take = 10;
  loading = false;
  error: string | null = null;

  constructor(private readonly vendorService: VendorService) {}

  ngOnInit(): void {
    this.load();
  }

  load(): void {
    this.loading = true;
    this.error = null;
    this.vendorService.getVendors(this.skip, this.take).subscribe({
      next: (vendors) => {
        this.vendors = vendors;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to load vendors.';
        this.loading = false;
        console.error(err);
      }
    });
  }

  next(): void {
    this.skip += this.take;
    this.load();
  }

  previous(): void {
    this.skip = Math.max(0, this.skip - this.take);
    this.load();
  }

  remove(id: string): void {
    if (!confirm(`Delete vendor ${id}?`)) {
      return;
    }
    this.vendorService.deleteVendor(id).subscribe({
      next: () => this.load(),
      error: (err) => {
        this.error = 'Failed to delete vendor.';
        console.error(err);
      }
    });
  }
}
