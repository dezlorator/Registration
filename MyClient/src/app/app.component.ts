import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
constructor(private router: Router, private http: HttpClient, private toaster: ToastrService){}
    goToRegistration(){
      this.router.navigate(['/registration']);
    }
    goToGame(){
      this.router.navigate(['/game']);
    }
    goToSignIn(){
      this.router.navigate(['/signIn']);
    }
    signOut(){
      localStorage.clear();
      this.http.get("https://localhost:44316/api/applicationUser/SingOut").subscribe(
        (result : any) =>{
          this.toaster.success("Successfully loged out");
        },
        error =>{
          this.toaster.error("Error");
        }
      )
    }
}
