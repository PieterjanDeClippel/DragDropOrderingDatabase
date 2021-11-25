import { Note } from "./Note";

export interface NoteSwapResult {
  note: Note;
  needsReindexing: boolean;
}
