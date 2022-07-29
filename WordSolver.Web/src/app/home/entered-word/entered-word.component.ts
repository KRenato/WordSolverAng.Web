import { Component, Input } from '@angular/core';
import { Word } from '../../models';

@Component({
  selector: 'app-entered-word',
  templateUrl: './entered-word.component.html',
  styleUrls: ['./entered-word.component.css'],
})
export class EnteredWordComponent {
  @Input() enteredWord: Word | undefined;
  @Input() isActiveWord: boolean | undefined;

  getLetters(word?: Word): string[] {
    if (!word) {
      return [];
    }

    return word.value.split('');
  }
}
