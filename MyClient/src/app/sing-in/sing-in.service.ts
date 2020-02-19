import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class SingInService {

  private url = "https://localhost:44316/api/applicationUser/SingIn";

    constructor(private http: HttpClient) {
    }

    singIn(singInInfo: any) {
        return this.http.post(this.url, singInInfo);
    }
}
