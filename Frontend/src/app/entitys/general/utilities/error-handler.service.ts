import { Injectable } from '@angular/core';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlerService {
  handle(error: any): void {
    const status = error.status;
    const message = error?.error?.message || 'Ha ocurrido un error inesperado';

    let title = 'Error';
    let icon: 'error' | 'warning' | 'info' = 'error';

    switch (status) {
      case 400:
        title = 'Solicitud inválida';
        break;
      case 404:
        title = 'No encontrado';
        break;
      case 500:
        title = 'Error del servidor';
        break;
      default:
        title = `Error ${status}`;
        break;
    }

    Swal.fire({
      title,
      text: message,
      icon,
      confirmButtonText: 'Aceptar'
    });
  }
}
