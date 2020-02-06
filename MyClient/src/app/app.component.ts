import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { RegistrationService } from './registration.service';
import { ToastrService } from 'ngx-toastr';
import { MyValidators } from './Validators/PasswordValidator';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  form: FormGroup;
  errorMessage: string;  


  constructor(private registrationService: RegistrationService, private toaster: ToastrService) { }

  ngOnInit(){
    this.form = new FormGroup({
      UserName: new FormControl('', [Validators.required]),
      FullName: new FormControl('', [Validators.required]),
      Email: new FormControl('', [Validators.required, Validators.email]),
      Password: new FormControl('', [Validators.required, Validators.minLength(4), MyValidators.isNumberExist]),
      ConfirmPassword: new FormControl('', [Validators.required])
    })
  }  

  onSubmit(){
    var user = {
      UserName: this.form.value.UserName,
      Email: this.form.value.Email,
      FullName: this.form.value.FullName,
      Password: this.form.value.Password,
      ConfirmPassword: this.form.value.ConfirmPassword
    }

    this.registrationService.register(user).subscribe(
      (result: any)=>{
        console.log("Good");
        this.toaster.success("User created");
      },
      error=>{
        console.log(error);
        switch(error.status){
          case 400:{
            this.errorMessage = "Wrong password";
            break;
          }
          case 409:{
            this.errorMessage = "This user name is already used";
            break;
          }
          default: {
            this.errorMessage = error.message;
          }
        }
        
        this.toaster.error(this.errorMessage)
      }
    );
  }
}
