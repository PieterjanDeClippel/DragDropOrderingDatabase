import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { Component } from '@angular/core';
import { map, ReplaySubject, tap } from 'rxjs';
import { Observable } from 'rxjs';
import { Subject } from 'rxjs';
import { BehaviorSubject } from 'rxjs';
import { Note } from './entities/Note';
import { NoteService } from './services/note/note.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  constructor(private noteService: NoteService) {
    this.numerators$ = this.notes$
      .pipe(map(notes => {
        return notes.map(note => note.numerator);
      }));

    this.largest$ = this.numerators$
      .pipe(map(numerators => {
        return numerators.sort((a, b) => {
          if (a < b) {
            return 1;
          } else if (a === b) {
            return 0;
          } else {
            return -1;
          }
        });
      })).pipe(map(numerators => {
        if (numerators.length < 2) {
          return [0, 0];
        } else {
          return numerators.slice(0, 2);
        }
      }));

    this.smallest$ = this.numerators$
      .pipe(map(numerators => {
        return numerators.sort((a, b) => {
          if (a > b) {
            return 1;
          } else if (a === b) {
            return 0;
          } else {
            return -1;
          }
        });
      })).pipe(map(numerators => {
        if (numerators.length === 0) {
          return 0;
        } else {
          return numerators[0];
        }
      }));


    this.noteService.getNotes().subscribe((notes) => {
      this.notes$.next(notes);
    });

  }

  title = 'ClientApp';
  notes$ = new BehaviorSubject<Note[]>([]);
  numerators$: Observable<number[]>;
  largest$: Observable<number[]>;
  smallest$: Observable<number>;
  newNoteText: string = '';
  numberOfSwaps: number = 0;
  needsReindexing: boolean = false;

  addNote(text: string) {
    this.noteService.createNote(text).subscribe((note) => {
      this.notes$.pipe(tap(notes => notes.push(note))).subscribe(() => null);
    });
  }

  noteDropped(event: CdkDragDrop<Note[]>) {
    if (event.previousContainer === event.container) {
      this.numberOfSwaps++;
      moveItemInArray(
        event.container.data,
        event.previousIndex,
        event.currentIndex);

      //console.log('event', event);

      this.noteService.swapNote(
        event.currentIndex === 0 ? null : this.notes$.value[event.currentIndex - 1],
        <Note>event.container.data[event.currentIndex],
        event.currentIndex === event.container.data.length - 1 ? null : this.notes$.value[event.currentIndex + 1]
      ).subscribe((noteSwapResult) => {
        let newState = this.notes$.value.map((item, index) => {
          if (index === event.currentIndex) {
            return noteSwapResult.note;
          } else {
            return item;
          }
        });
        this.needsReindexing = noteSwapResult.needsReindexing;
        this.notes$.next(newState);
      });
    } else {
      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex);
    }
  }
}
