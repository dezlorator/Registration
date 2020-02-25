import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms'
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http'
import { ToastrModule } from 'ngx-toastr';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations' 
import { CommonModule } from '@angular/common';
import { AppComponent } from './app.component';
import { RegistrationService } from './registration/registration.service';
import { GuessWhatGoogleGameComponent } from './guess-what-google-game/guess-what-google-game.component';
import { AppRoutingModule } from './app-routing.module';
import { RegistrationComponent } from './registration/registration.component';
import { GuessWhatGoogleGameService } from './guess-what-google-game/guess-what-google-game.service';
import { SingInComponent } from './sing-in/sing-in.component';
import { SingInService } from './sing-in/sing-in.service';
import { AuthHttpInterceptor } from './sing-in/httpInterceptor';
import { StorageServiceModule } from "ngx-webstorage-service";
import { AuthInterceptor } from './sing-in/auth.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    GuessWhatGoogleGameComponent,
    RegistrationComponent,
    SingInComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    AppRoutingModule,
    StorageServiceModule
  ],
  providers: [RegistrationService, GuessWhatGoogleGameService, SingInService,
  {
    provide: HTTP_INTERCEPTORS,
    useClass: AuthHttpInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
