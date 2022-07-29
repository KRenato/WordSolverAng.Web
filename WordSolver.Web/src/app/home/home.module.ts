import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home/home.component';
import { EnteredWordComponent } from './entered-word/entered-word.component';

@NgModule({
  declarations: [HomeComponent, EnteredWordComponent],
  imports: [CommonModule],
})
export class HomeModule {}
