import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { NgFor, NgIf } from '@angular/common';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { PersonService } from '../../../services/person.service';
import { UserService } from '../../../services/user.service';
import { CustomValidators } from '../../general/utilities/validators';
import { ErrorHandlerService } from '../../general/utilities/error-handler.service';


@Component({
  selector: 'app-form-user',
  standalone: true,
  imports: [ReactiveFormsModule, MatFormFieldModule, MatInputModule, MatSelectModule, MatButtonModule, MatIconModule, NgFor, NgIf, MatIconModule, MatTooltipModule],
  templateUrl: './create-user.component.html',
  styleUrl: './create-user.component.css'
})
export class CreateUserComponent implements OnInit {

  private formBuilder = inject(FormBuilder);
  private userService = inject(UserService);
  private errorHandler = inject(ErrorHandlerService)



  private personService = inject(PersonService);
  private router = inject(Router);
  private snackBar = inject(MatSnackBar);
  
  isAdmin = false;
  persons: any[] = [];
  noPersonsAvailable = false;
  hidePassword = true;

  form = this.formBuilder.group({
    username: ['', [Validators.required, Validators.minLength(3)]],
    password: ['', [ Validators.required, Validators.minLength(8), CustomValidators.strongPassword()]],
    personId: [null, Validators.required],
    active: [true],
  });

  ngOnInit(): void {
    this.loadPersons();
  }

  private loadPersons(): void {
    this.personService.getAvailable().subscribe({
      next: (data) => {
        this.persons = data;
        if (this.persons.length === 0) {
          this.noPersonsAvailable = true;
          this.form.disable();
        }
      },
      error: (err) => {
        this.errorHandler.handle(err)
      }
    });
  }

  submit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.userService.create(this.form.value).subscribe({
      next: () => {
        this.snackBar.open('Usuario registrado exitosamente', 'Cerrar', {
          duration: 3000
        });
        this.router.navigate(['/user']);
      },
      error: (err) => {
        this.errorHandler.handle(err)
      }
    });
  }

  cancelar(): void {
    this.router.navigate(['/user']);
  }
}