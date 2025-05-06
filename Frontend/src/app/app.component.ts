import { NgIf } from '@angular/common';
import { Component, inject } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { MenuComponent } from "./menu/menu.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NgIf, MenuComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  private router = inject(Router);
  
  get mostrarMenu(): boolean {
    const rutasOcultas = ['/login', '/register'];
    return !rutasOcultas.some(r => this.router.url.startsWith(r));
  }
}
