import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { InterceptorService } from './interceptor.service';
import { HomeComponent } from './home/home.component';
import { RegistrationComponent } from './registration/registration.component';
import { RecipeComponent } from './recipe/recipe.component';
import { AdminHomeComponent } from './admin-home/admin-home.component';
import { RecipeEditorComponent } from './recipe-editor/recipe-editor.component';


const routes: Routes = [
  { path: 'register', component: RegistrationComponent },
  { path: 'recipeDetail/:id', component: RecipeComponent },
  { path: 'recipeEdit/:id', component: RecipeEditorComponent },
  { path: 'admin', component: AdminHomeComponent },
  { path: '**', component: HomeComponent },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: InterceptorService,
      multi: true
    }
  ]
})
export class AppRoutingModule { }