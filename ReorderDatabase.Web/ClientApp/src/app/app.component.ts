import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { Component } from '@angular/core';
import { Note } from './entities/Note';
import { NoteService } from './services/note/note.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  constructor(private noteService: NoteService) {
    this.noteService.getNotes().subscribe((notes) => {
      this.notes = notes;
    });
  }

  title = 'ClientApp';
  notes: Note[] = [];
  newNoteText: string = '';
  numberOfSwaps: number = 0;

  addNote(text: string) {
    this.noteService.createNote(text).subscribe((note) => {
      this.notes.push(note);
    });
  }

  noteDropped(event: CdkDragDrop<Note[]>) {
    if (event.previousContainer === event.container) {
      this.numberOfSwaps++;
      moveItemInArray(
        event.container.data,
        event.previousIndex,
        event.currentIndex);

      console.log('event', event);

      this.noteService.swapNote(
        event.currentIndex === 0 ? null : this.notes[event.currentIndex - 1],
        <Note>event.container.data[event.currentIndex],
        event.currentIndex === event.container.data.length - 1 ? null : this.notes[event.currentIndex + 1]
      ).subscribe((note) => {
        this.notes[event.currentIndex] = note;
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
