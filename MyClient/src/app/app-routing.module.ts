import {NgModule} from '@angular/core'
import {RouterModule, Routes} from '@angular/router'
import { GuessWhatGoogleGameComponent } from './guess-what-google-game/guess-what-google-game.component'
import { AppComponent } from './app.component'

const routes: Routes = [
  {path: 'game', component: GuessWhatGoogleGameComponent},
  {path: 'registration', component: AppComponent}
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {

}
