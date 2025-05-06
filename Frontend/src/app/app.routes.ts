import { Routes } from '@angular/router';
import { LandingComponent } from './landing/landing.component';
import { IndiceUserComponent } from './entitys/user/indice-user/indice-user.component';
import { CreateUserComponent } from './entitys/user/create-user/create-user.component';
import { UpdateUserComponent } from './entitys/user/update-user/update-user.component';

export const routes: Routes = [
    {path: '', component:LandingComponent},

    // User
    { path: 'user', component: IndiceUserComponent },
    { path: 'user/create', component: CreateUserComponent },
    { path: 'user/update/:id', component: UpdateUserComponent },
];
