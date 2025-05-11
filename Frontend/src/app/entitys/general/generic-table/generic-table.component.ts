import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-generic-table',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatTableModule, MatIconModule, MatButtonModule, RouterLink],
  templateUrl: './generic-table.component.html',
  styleUrl: './generic-table.component.css',
})
export class GenericTableComponent {
  @Input() title: string = 'Listado';
  @Input() data: any[] = [];
  @Input() displayedColumns: string[] = [];
  @Input() columnDefs: { key: string, label: string, render?: (item: any, index: number) => string }[] = [];
  @Input() showRegisterButton = false;
  @Input() registerRoute = '';
  @Input() editRouteBase: string = ''; 
  @Input() showActions = true;  


  @Output() delete = new EventEmitter<any>();
}
