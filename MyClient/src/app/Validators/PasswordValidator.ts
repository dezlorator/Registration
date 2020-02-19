import {FormControl, AbstractControl} from '@angular/forms'

export class MyValidators {

    static isNumberExist(control: FormControl): {[key: string]: boolean} {
      let passsord = control.value;
      for(var n = 0; n < control.value.length; n++){
        if(passsord.charCodeAt(n) >= 47 && passsord.charCodeAt(n) <= 58){
          return null
        }
      }

      return {numberIsNotExist: true}
    }
    
    static isUpperLetterExist(control: FormControl): {[key: string]: boolean} {
      let passsord = control.value;
      for(var n = 0; n < control.value.length; n++){
        if(passsord.charCodeAt(n) >= 65 && passsord.charCodeAt(n) <= 90){
          return null
        }
      }

      return {upperLetterIsNotExist: true}
    }

    static isLoverLetterExist(control: FormControl): {[key: string]: boolean} {
      let passsord = control.value;
      for(var n = 0; n < control.value.length; n++){
        if(passsord.charCodeAt(n) >= 97 && passsord.charCodeAt(n) <= 122){
          return null
        }
      }

      return {loverLetterIsNotExist: true}
    }
  }
  