import { Routes } from '@angular/router';
import { LandingComponent } from './landing/landing.component';
import { IndiceUserComponent } from './entitys/user/indice-user/indice-user.component';

export const routes: Routes = [
    {path: '', component:LandingComponent},

    // User
    // User
    { path: 'user', component: IndiceUserComponent },
];
