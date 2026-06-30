import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { Vendor } from '../models/vendor.model';
import { VendorService } from '../services/vendor.service';

@Component({
  selector: 'app-vendor-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './vendor-form.component.html',
  styleUrl: './vendor-form.component.scss'
})
export class VendorFormComponent implements OnInit {
  isEdit = false;
  saving = false;
  error: string | null = null;

  form = this.fb.group({
    id: ['', Validators.required],
    description: ['', Validators.required],
    address: ['', Validators.required]
  });

  constructor(
    private readonly fb: FormBuilder,
    private readonly vendorService: VendorService,
    private readonly route: ActivatedRoute,
    private readonly router: Router
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isEdit = true;
      this.form.controls.id.disable();
      this.vendorService.getVendor(id).subscribe({
        next: (vendor) => this.form.patchValue(vendor),
        error: (err) => {
          this.error = 'Failed to load vendor.';
          console.error(err);
        }
      });
    }
  }

  save(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.saving = true;
    this.error = null;
    const vendor = this.form.getRawValue() as Vendor;

    const request = this.isEdit
      ? this.vendorService.updateVendor(vendor)
      : this.vendorService.addVendor(vendor);

    request.subscribe({
      next: () => this.router.navigate(['/vendors']),
      error: (err) => {
        this.error = 'Failed to save vendor.';
        this.saving = false;
        console.error(err);
      }
    });
  }

  cancel(): void {
    this.router.navigate(['/vendors']);
  }
}
