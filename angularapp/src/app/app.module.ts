import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

// import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { HomeComponent } from './home/home.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
// import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RecipeComponent } from './recipe/recipe.component';
import { RegistrationComponent } from './registration/registration.component';
import { RecipeSaver } from './recipe/recipe-saver';
import { RecipeGuideComponent } from './recipe/recipe-guide/recipe-guide.component';

@NgModule({
  declarations: [
    AppComponent,
    RegistrationComponent,
    HomeComponent,
    RecipeComponent,
    RecipeGuideComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [RecipeSaver],
  bootstrap: [AppComponent]
})
export class AppModule { }
