import {FormControl, AbstractControl} from '@angular/forms'

export class MyValidators {

    static isNumberExist(control: FormControl): {[key: string]: boolean} {
      var passsord = control.value;
      for(var n = 0; n < passsord.Length; n++){
        if(passsord.charCodeAt(n) > 47 && passsord.charCodeAt(n) < 58){
          return null
        }
      }

      return {numberIsNotExist: true}
    }
  }
  