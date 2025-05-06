import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment.development';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PersonService {

  private baseUrl = environment.apiURL + 'api/Person/';

  constructor(private http: HttpClient) {}

  getAll(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}GetAll/`);
  }

  getAvailable(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + 'GetAvailable/');
  }

  getById(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}GetById/${id}/`);
  }

  create(person: any): Observable<any> {
    return this.http.post(`${this.baseUrl}Create/`, person);
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
