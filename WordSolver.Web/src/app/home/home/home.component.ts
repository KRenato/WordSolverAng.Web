import { Component, OnInit } from '@angular/core';
import { ApiService, ConfigService } from 'src/app/services';
import { Word } from '../../models/';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  pageLoaded: boolean = false;

  numberOfGuesses!: number;
  messageText: string = '';

  enteredWords: Word[] = [];

  constructor(
    private configService: ConfigService,
    private apiService: ApiService
  ) {}

  ngOnInit() {
    this.initialize();
  }

  getNextWordClicked() {
    const lastWord = this.enteredWords[this.enteredWords.length - 1];

    if (lastWord.isExactMatch()) {
      this.messageText = 'Aww yissss....';
      return;
    }

    this.getNextWord();
  }

  startOverClicked() {
    this.initialize();
  }

  private initialize() {
    this.configService.get().subscribe(config => {
      this.numberOfGuesses = config.numberOfGuesses;
      this.enteredWords = [new Word(config.initialWord)];
      this.messageText = '';
      this.pageLoaded = true;
    });
  }

  private getNextWord() {
    this.apiService.getBestWord(this.enteredWords).subscribe(word => {
      if (!word) {
        this.messageText = 'Sorry, better luck next time.';
        return;
      }

      this.enteredWords.push(new Word(word));
    });
  }
}
