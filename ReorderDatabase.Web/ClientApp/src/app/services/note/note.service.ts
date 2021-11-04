import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseUrlService } from '../base-url/base-url.service';
import { Note } from '../../entities/Note';

@Injectable({
  providedIn: 'root'
})
export class NoteService {

  constructor(
    private httpClient: HttpClient,
    private baseUrlService: BaseUrlService
  ) { }

  public getNotes() {
    return this.httpClient.get<Note[]>(`${this.baseUrlService.baseUrl}/web/Note`);
  }

  public getNote(id: number) {
    return this.httpClient.get<Note>(`${this.baseUrlService.baseUrl}/web/Note/${id}`);
  }

  public createNote(text: string) {
    return this.httpClient.post<Note>(`${this.baseUrlService.baseUrl}/web/Note`, { text: text });
  }

  public updateNote(note: Note) {
    return this.httpClient.put<Note>(`${this.baseUrlService.baseUrl}/web/Note/${note.id}`, note);
  }

  public swapNote(noteBefore: Note | null, note: Note, noteAfter: Note | null) {
    return this.httpClient.put<Note>(`${this.baseUrlService.baseUrl}/web/Note/swap`, {
      noteBefore,
      note,
      noteAfter
    });
  }

  public deleteNote(note: Note) {
    return this.httpClient.delete(`${this.baseUrlService.baseUrl}/web/Note/${note.id}`);
  }
}
