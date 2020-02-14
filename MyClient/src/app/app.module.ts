import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms'
import {HttpClientModule} from '@angular/common/http'
import { ToastrModule } from 'ngx-toastr';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations' 
import { CommonModule } from '@angular/common';
import { AppComponent } from './app.component';
import { RegistrationService } from './registration/registration.service';
import { GuessWhatGoogleGameComponent } from './guess-what-google-game/guess-what-google-game.component';
import { AppRoutingModule } from './app-routing.module';
import { RegistrationComponent } from './registration/registration.component';
import { GuessWhatGoogleGameService } from './guess-what-google-game/guess-what-google-game.service';

@NgModule({
  declarations: [
    AppComponent,
    GuessWhatGoogleGameComponent,
    RegistrationComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    AppRoutingModule
  ],
  providers: [RegistrationService, GuessWhatGoogleGameService],
  bootstrap: [AppComponent]
})
export class AppModule { }
