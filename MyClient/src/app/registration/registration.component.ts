import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { RegistrationService } from './registration.service';
import { MyValidators } from '../Validators/PasswordValidator';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {
  form: FormGroup;
  errorMessage: string;  

  constructor(private registrationService: RegistrationService, private toaster: ToastrService) { }

  ngOnInit(){
    this.form = new FormGroup({
      UserName: new FormControl('', [Validators.required]),
      FullName: new FormControl('', [Validators.required]),
      Email: new FormControl('', [Validators.required, Validators.email]),
      Password: new FormControl('', [Validators.required, Validators.minLength(6), MyValidators.isNumberExist,
      MyValidators.isLoverLetterExist, MyValidators.isUpperLetterExist]),
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
