import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { GenericTableComponent } from '../../general/generic-table/generic-table.component';
import { UserService } from '../../../services/user.service';
import Swal from 'sweetalert2';
import { ErrorHandlerService } from '../../general/utilities/error-handler.service';

@Component({
  selector: 'app-indice-user',
  standalone: true,
  imports: [CommonModule, GenericTableComponent],
  templateUrl: './indice-user.component.html',
  styleUrl: './indice-user.component.css'
})
export class IndiceUserComponent implements OnInit {
  private userService = inject(UserService);
  private errorHandler = inject(ErrorHandlerService)

  users: any[] = [];
  columnas = ['id', 'username', 'password', 'personName', 'status', 'acciones'];
  columnDefs = [
    { key: 'id', label: 'N°', render: (_: any, i: number) => (i + 1).toString() },
    { key: 'username', label: 'Nombre' },
    { key: 'password', label: 'Contraseña' },
    { key: 'personName', label: 'Persona' },
    { key: 'status', label: 'Estado', render: (item: any) => item.active ? 'Activo' : 'Inactivo' }
  ];
  isAdmin = false;

  ngOnInit(): void {
    this.cargarUsers();
  }

  cargarUsers(): void {
    this.userService.getAll().subscribe({
      next: data => this.users = data,
      error: err => this.errorHandler.handle(err)
    });
  }

  eliminarUser(user: any) {
    Swal.fire({
      title: '¿Qué tipo de eliminación deseas?',
      text: `Usuario: ${user.username}`,
      icon: 'warning',
      showCancelButton: true,
      showDenyButton: true,
      confirmButtonText: 'Lógica',
      denyButtonText: 'Permanente',
      cancelButtonText: 'Cancelar',
      confirmButtonColor: '#3085d6',
      denyButtonColor: '#d33',
    }).then(result => {
      if (result.isConfirmed) {
        this.userService.deleteLogical(user.id).subscribe({
          next: () => {
            Swal.fire('Eliminado lógicamente', '', 'success');
            this.cargarUsers();
          },
          error: err => this.errorHandler.handle(err)
        });
      } else if (result.isDenied) {
        this.userService.delete(user.id).subscribe({
          next: () => {
            Swal.fire('Eliminado permanentemente', '', 'success');
            this.cargarUsers();
          },
          error: err => this.errorHandler.handle(err)
        });
      }
    });
  }
}

