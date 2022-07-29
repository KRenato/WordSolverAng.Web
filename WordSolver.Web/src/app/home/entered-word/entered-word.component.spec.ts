import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Word } from 'src/app/models';

import { EnteredWordComponent } from './entered-word.component';

describe('EnteredWordComponent', () => {
  let component: EnteredWordComponent;
  let fixture: ComponentFixture<EnteredWordComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EnteredWordComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(EnteredWordComponent);
    component = fixture.componentInstance;
    component.enteredWord = new Word('test');
    component.isActiveWord = false;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('getLetters(undefined) should return empty array', () => {
    const value = component.getLetters(undefined);
    expect(value).toEqual([]);
  });

  it('getLetters(new Word("arose")) should return a string array', () => {
    const word = new Word('arose');
    const value = component.getLetters(word);
    expect(value).toEqual(['a', 'r', 'o', 's', 'e']);
  });
});
