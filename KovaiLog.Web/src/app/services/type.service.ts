import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Type } from "../models";
import { Observable } from "rxjs/index";

@Injectable({
  providedIn: 'root'
})
export class TypeService {

  baseUrl = environment.apiUrl + "/api/TypeMaster";

  constructor(private http: HttpClient) { }

  getTypes(): Observable<Type[]> {
    return this.http.get<Type[]>(this.baseUrl);
  }

  getTypeById(id: number): Observable<Type> {
    return this.http.get<Type>(this.baseUrl + "/" + id);
  }

  createType(Type: Type): Observable<Type> {
    return this.http.post<Type>(this.baseUrl, Type);
  }

  updateType(Type: Type): Observable<Type> {
    return this.http.put<Type>(this.baseUrl + "/" + Type.TypeId, Type);
  }

  deleteType(id: number): Observable<Type> {
    return this.http.delete<Type>(this.baseUrl + "/" + id);
  }
}
