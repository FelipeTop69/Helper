import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.apiURL}api/User/`;

  getAll(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}GetAll/`);
  }


  getById(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}GetByiId/${id}/`);
  }

  create(user: any): Observable<any> {
    return this.http.post(`${this.baseUrl}Create/`, user);
  }

  update(user: any): Observable<any> {
    return this.http.put(`${this.baseUrl}Update/`, user);
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}Persistence/${id}`);
  }

  deleteLogical(id: number): Observable<any> {
    return this.http.patch(`${this.baseUrl}Logical/${id}`, {
      active: false
    });
  }
}
