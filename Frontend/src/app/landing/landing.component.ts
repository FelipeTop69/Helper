import { Component, inject } from '@angular/core';
import { TestConnectionService } from '../test-connection.service';

@Component({
  selector: 'app-landing',
  imports: [],
  templateUrl: './landing.component.html',
  styleUrl: './landing.component.css'
})
export class LandingComponent {
  testConnection = inject(TestConnectionService);
  users: any[] = [];

  constructor(){
    this.testConnection.obtenerUsers().subscribe(datos => {
      this.users = datos;
    })
  }
}
