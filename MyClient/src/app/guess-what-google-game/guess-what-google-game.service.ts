import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class GuessWhatGoogleGameService {

  private urlToGetQuestion = "https://localhost:44316/api/guessWhatGoogleGame/Question";
  private urlToSendAnswer = "https://localhost:44316/api/guessWhatGoogleGame/GetAnswer"
    constructor(private http: HttpClient) {
    }

    getQuestion() {
        return this.http.get(this.urlToGetQuestion);
    }
    sendUserAnswer(userAnswer: any){
      return this.http.post(this.urlToSendAnswer, userAnswer);
    }
}
