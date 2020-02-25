import { HttpInterceptor, HttpRequest, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';


@Injectable()
export class AuthInterceptor implements HttpInterceptor{
    intercept(req: import("@angular/common/http").HttpRequest<any>, next: import("@angular/common/http").HttpHandler): import("rxjs").Observable<import("@angular/common/http").HttpEvent<any>> {
        const token = localStorage.getItem("token");

        if(token){
            let authReq = req.clone({body: this.setAuthHeaders(req)});
            return next.handle(authReq);
        }
        else{
            return next.handle(req);
        }
    }

    private setAuthHeaders(req: HttpRequest<any>): HttpHeaders{
        const headerSetting: {[name:string]: string | string[];} = {};
        for(const key of req.headers.keys()){
            headerSetting[key] = req.headers.getAll(key);
        }
        headerSetting['Authorization'] = localStorage.getItem("token");
        return new HttpHeaders(headerSetting);
    }
}