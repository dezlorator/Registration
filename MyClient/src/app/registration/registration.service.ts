import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class RegistrationService {

  private url = "https://localhost:44316/api/applicationUser/Register";

    constructor(private http: HttpClient) {
    }

    register(user: any) {
        return this.http.post(this.url, user);
    }
}
