import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Content } from "../models/content";
import { Observable } from "rxjs/index";


@Injectable({
  providedIn: 'root'
})
export class ContentService {

  baseUrl = environment.apiUrl + "/api/Content";

  constructor(private http: HttpClient) { }

  getContents(): Observable<Content[]> {
    return this.http.get<Content[]>(this.baseUrl);
  }

  getContentById(id: number): Observable<Content> {
    return this.http.get<Content>(this.baseUrl + "/" + id);
  }

  createContent(content: Content): Observable<Content> {
    return this.http.post<Content>(this.baseUrl, content);
  }

  updateContent(content: Content): Observable<Content> {
    return this.http.put<Content>(this.baseUrl + "/" + content.ContentId, content);
  }

  deleteContent(id: number): Observable<Content> {
    return this.http.delete<Content>(this.baseUrl + "/" + id);
  }
}
