import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable()
export class AuthenticationService {
    private apiUrl: string;

    constructor(private http: HttpClient) {
        this.apiUrl = 'api/users/authenticate';
     }

    isLogged(): boolean {
        if (localStorage.getItem('currentUser')) {
            return true;
        }
    }

    login(username: string, password: string) {
        return this.http.post<any>(this.apiUrl, { username: username, password: password })
            .pipe(map(user => {
                // login successful if there's a jwt token in the response
                if (user && user.token) {
                    // store user details and jwt token in local storage to keep user logged in between page refreshes
                    localStorage.setItem('currentUser', JSON.stringify(user));
                }

                return user;
            }));
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
    }
}
