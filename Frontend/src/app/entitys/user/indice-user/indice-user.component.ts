import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { GenericTableComponent } from '../../general/generic-table/generic-table.component';
import { UserService } from '../../../services/user.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-indice-user',
  imports: [CommonModule, GenericTableComponent],
  templateUrl: './indice-user.component.html',
  styleUrl: './indice-user.component.css'
})
export class IndiceUserComponent implements OnInit {
  private userService = inject(UserService);

  users: any[] = [];
  columnas = ['id', 'username', 'password', 'personName', 'status', 'acciones'];
  columnDefs = [
    { key: 'id', label: 'N°', render: (_: any, i: number) => (i + 1).toString() },
    { key: 'username', label: 'Nombre' },
    { key: 'password', label: 'Contraseña' },
    { key: 'personName', label: 'Persona' },
    { key: 'status', label: 'Estado', render: (item: any) => item.status ? 'Activo' : 'Inactivo' }
  ];
  isAdmin = false;

  ngOnInit(): void {
    this.cargarUsers();
  }

  cargarUsers(): void {
    this.userService.getAll().subscribe({
      next: data => this.users = data,
      error: err => console.error("Error cargando users", err)
    });
  }

  editar(user: any) {
    // Puedes redirigir directamente o usar routerLink en la tabla
    console.log('Editar', user);
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
        this.userService.deleteLogical(user.id).subscribe(() => {
          Swal.fire('Eliminado lógicamente', '', 'success');
          this.cargarUsers();
        });
      } else if (result.isDenied) {
        this.userService.delete(user.id).subscribe(() => {
          Swal.fire('Eliminado permanentemente', '', 'success');
          this.cargarUsers();
        });
      }
    });
  }
}

