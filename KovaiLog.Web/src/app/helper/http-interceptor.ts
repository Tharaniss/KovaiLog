import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { AuthenticationService } from '../services/authentication.service';

@Injectable()
export class HttpClientInterceptor implements HttpInterceptor {
    constructor(private authenticationService: AuthenticationService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const currentUser: any = localStorage.getItem('currentUser') && JSON.parse(localStorage.getItem('currentUser'));
        const token = currentUser && currentUser.access_token;
;
        if (token) {
            request = request.clone({ headers: request.headers.set('Authorization', 'Bearer ' + token) });
        }

        if (!request.headers.has('Content-Type')) {
            request = request.clone({ headers: request.headers.set('Content-Type', 'application/json') });
        }

        request = request.clone({ headers: request.headers.set('Accept', 'application/json') });
        return next.handle(request).pipe(map((event: HttpEvent<any>) => {
            if (event instanceof HttpResponse) {
                console.log(event);
            }
            return event;
            }),catchError(err => {
            if (err.status === 401) {
                // auto logout if 401 response returned from api
                this.authenticationService.logout();
                location.reload(true);
            }
            let validationErrors = "";
            for (var errorArray in err.error.ModelState) {
                if (err.error.ModelState.hasOwnProperty(errorArray )) {
                    validationErrors = validationErrors.concat(err.error.ModelState[errorArray]);
                }
             }
            const error = (((validationErrors || err.error.error_description) || err.error.message )|| err.statusText);
            return throwError(error);
        }))
    }
}