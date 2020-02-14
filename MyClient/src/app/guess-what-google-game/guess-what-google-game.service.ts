import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class GuessWhatGoogleGameService {

  private urlToGetId = "https://localhost:44316/api/guessWhatGoogleGame/Question";
    constructor(private http: HttpClient) {
    }

    getQuestion() {
        return this.http.get(this.urlToGetId);
    }
}
