import { Injectable } from "@angular/core";
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpResponse, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class AuthHttpInterceptor implements HttpInterceptor{
    
    public intercept(req: HttpRequest<any>, next: HttpHandler) : Observable<HttpEvent<any>>{
        var token = localStorage.getItem("token");
        if(token){
            const authReq = req.clone({headers: this.setAuthHeaders(req, token)});
            return next.handle(authReq);
        }

        return next.handle(req);
    }

    private setAuthHeaders(req: HttpRequest<any>, token: string): HttpHeaders{
        const headerSetting: {[name:string]: string | string[];} = {};
        for(const key of req.headers.keys()){
            headerSetting[key] = req.headers.getAll(key);
        }
        headerSetting['Authorization'] = token;
        return new HttpHeaders(headerSetting);
    }
} 
