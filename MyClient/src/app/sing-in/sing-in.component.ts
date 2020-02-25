import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { SingInService } from './sing-in.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-sing-in',
  templateUrl: './sing-in.component.html',
  styleUrls: ['./sing-in.component.scss']
})
export class SingInComponent implements OnInit {
  form: FormGroup;
  constructor(private service : SingInService, private toaster: ToastrService) { }

  ngOnInit() {
    this.form = new FormGroup({
      UserName: new FormControl('', [Validators.required]),
      Password: new FormControl('', [Validators.required])
    })
  }
  onSubmit(){
    var singInInfo = {
      Password: this.form.value.Password,
      UserName: this.form.value.UserName
    }

    this.service.singIn(singInInfo).subscribe(
      (result: any) =>{
        localStorage.setItem("token", result.body.encodedJwt);
        this.toaster.success("Sing in completed");
      },
      error =>{
        console.log(error);
        this.toaster.error("Wrong user name or password");
      }
    )
  }
}
