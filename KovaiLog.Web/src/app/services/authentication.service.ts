import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Router } from "@angular/router";

import { environment } from '../../environments/environment';
import { User, UserToken } from '../models/user';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
  private currentUserSubject: BehaviorSubject<User>;
  public currentUser: Observable<User>;

  constructor(private http: HttpClient, private router: Router) {
    this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): User {
    return this.currentUserSubject.value;
  }

  login(username: string, password: string, isSocial?: boolean, socialName?: string) {

    let options1 = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };    
    // this.http.get<any>(`${environment.apiUrl}/api/User`, options1)
    //   .pipe(map(user => {
    //     // store user details and jwt token in local storage to keep user logged in between page refreshes
        
    //   })).subscribe();

    var data = "grant_type=password&username="+ username + "&password="+ password ;
    if(isSocial)
    {
      var data = "grant_type=password&username="+ username + "&password="+ password + "&provider=Google&name="+ socialName;
    }
    let options = {
      headers: new HttpHeaders({ 'Content-Type': 'application/x-www-urlencoded' })
    }; 
    return this.http.post<any>(`${environment.apiUrl}/token`, data, options)
      .pipe(map(UserToken => {
        // store user details and jwt token in local storage to keep user logged in between page refreshes
        localStorage.setItem('currentUser', JSON.stringify(UserToken));
        this.currentUserSubject.next(UserToken);
        return UserToken;
      },));
  }

  logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
    this.router.navigate(["login"]);
  }
}