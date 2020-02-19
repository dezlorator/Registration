import {NgModule} from '@angular/core'
import {RouterModule, Routes} from '@angular/router'
import { GuessWhatGoogleGameComponent } from './guess-what-google-game/guess-what-google-game.component'
import { AppComponent } from './app.component'
import { RegistrationComponent } from './registration/registration.component'
import { SingInComponent } from './sing-in/sing-in.component'

const routes: Routes = [
  {path: 'game', component: GuessWhatGoogleGameComponent},
  {path: 'registration', component: RegistrationComponent},
  {path:'signIn', component: SingInComponent}
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {

}
