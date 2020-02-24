import { Injectable } from "@angular/core";
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpResponse, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';


export enum StorageType{
    localStorage = 'localStorage',
    sessionStorage = 'sessionStorage'
}

export const WebStorage = (name: string = StorageType.localStorage): any=>{
    const nameStorage = (name == StorageType.localStorage || name == StorageType.sessionStorage)
    ? name
    :StorageType.localStorage;
    return (target: Object, propertyKey: string): void =>{
        Object.defineProperty(target, propertyKey, {
            get: () => JSON.parse(window[nameStorage].getItem(propertyKey)) || '',
            set(newValue: string){
                try{
                    window[nameStorage].setItem(propertyKey, JSON.stringify(newValue));
                }
                catch(e){
                    console.error(e);
                }
            },
            enumerable: true,
            configurable: true
        });
    }
}

@Injectable()
export class AuthHttpInterceptor implements HttpInterceptor{
    @WebStorage(StorageType.localStorage) public token : string;
    
    public intercept(req: HttpRequest<any>, next: HttpHandler) : Observable<HttpEvent<any>>{
        if(this.token){
            const authReq = req.clone({headers: this.setAuthHeaders(req)});
            return next.handle(authReq);
        }

        return next.handle(req);
    }

    private setAuthHeaders(req: HttpRequest<any>): HttpHeaders{
        const headerSetting: {[name:string]: string | string[];} = {};
        for(const key of req.headers.keys()){
            headerSetting[key] = req.headers.getAll(key);
        }
        headerSetting['Authorization'] = 'Bearer '+this.token;
        return new HttpHeaders(headerSetting);
    }
} 
