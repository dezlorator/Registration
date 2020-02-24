import { HttpInterceptor } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable()
export class AuthInterceptor implements HttpInterceptor{
    intercept(req: import("@angular/common/http").HttpRequest<any>, next: import("@angular/common/http").HttpHandler): import("rxjs").Observable<import("@angular/common/http").HttpEvent<any>> {
        const idToken = localStorage.getItem("token");

        if(idToken){
            const cloned = req.clone({
                headers: req.headers.set("Authorization",
                "Bearer" + idToken)
            });

            return next.handle(cloned);
        }
        else{
            return next.handle(req);
        }
    }
}