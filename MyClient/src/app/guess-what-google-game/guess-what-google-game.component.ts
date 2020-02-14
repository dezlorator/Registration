import { Component, OnInit } from '@angular/core';
import { GuessWhatGoogleGameService } from './guess-what-google-game.service';
import { FormGroup, FormBuilder, FormArray, FormControl } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-guess-what-google-game',
  templateUrl: './guess-what-google-game.component.html',
  styleUrls: ['./guess-what-google-game.component.scss']
})
export class GuessWhatGoogleGameComponent implements OnInit {
answers: string[];
idContainer: number[];
iterator: number = 0;
errorMessage: string;
question: string;
pictureUrl: string;
form: FormGroup;
ordersData = [
  { id: 0, name: '' },
  { id: 1, name: '' },
  { id: 2, name: '' },
  { id: 3, name: '' }
];

  constructor(private service: GuessWhatGoogleGameService, private formBuilder: FormBuilder,
  private toaster: ToastrService) {
    this.form = this.formBuilder.group({
      orders: new FormArray([])
    });

    this.addCheckboxes();
  }

  ngOnInit() {
    this.GetQuestion();
  }

  private addCheckboxes() {
    this.ordersData.forEach((o, i) => {
      const control = new FormControl(i === 0); // if first item set to true, else false
      (this.form.controls.orders as FormArray).push(control);
    });
  }

  submit() {
    var selected;
    var i = 0;
    var counter = 0;
    for(i; i < this.form.value.orders.length; i++){
      if(this.form.value.orders[i] == true){
        selected = i;
        counter++;
      }
    }
    if(counter > 1){
      this.toaster.warning("You should choose only one answer");
    }
    else if(this.ordersData[selected].name == this.question){
      this.toaster.success("You are right");
    }
    else{
      this.toaster.warning("You are wrong");
    }
  }
  GetQuestion(){
    this.service.getQuestion().subscribe(
      (result: any)=>{
        this.pictureUrl = result.imageUrl;
        this.ordersData[0].name = result.answers[0];
        this.ordersData[1].name = result.answers[1];
        this.ordersData[2].name = result.answers[2];
        this.ordersData[3].name = result.answers[3];
        this.question = result.questionString;
        console.log(result);
      },
      error =>{
        console.log(error);
        switch(error.status){
          case 404:{
            this.errorMessage = "Sorry no answer found";
            break;
          }
          case 204:{
            this.errorMessage = "Google has no images for this request";
            break;
          }
          default: {
            this.errorMessage = error.message;
          }
        }

          this.toaster.error(this.errorMessage);
      }
    )
  }

}
