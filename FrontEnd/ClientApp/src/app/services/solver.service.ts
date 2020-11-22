import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of, from, throwError } from 'rxjs';
import { InputData, OutputData } from '../models' 
@Injectable({
  providedIn: 'root'
})

export class SolverService {

    constructor(private http: HttpClient) { }
    readonly  apiUrl : string  = `https://localhost:44336/api/game`;

    solveGame (tiles: number[]) : Observable<OutputData> {
        let arraySize:number = tiles.length;
        let unusedValues :number[] = new Array();

        for (let i = 0; i < arraySize; i++)
        {
            unusedValues.push(i);
        }
        for (let num of tiles)
        {
            if (unusedValues.includes(num)){
                let index = unusedValues.indexOf(num);
                unusedValues.splice(index, 1);
            }
            else{
                return throwError("The value "+num+" is used more than once.");
            }
        }
        let inputData: InputData = new InputData(tiles);
        let outputData: OutputData;
        let fieldObservable: Observable<number[]>;
        return this.http.post<OutputData>(`${this.apiUrl}/solvePuzzle`, inputData);   
    }
    sequenceSubscriber(observer, output: OutputData) {
        let timeoutId;  
        let tiles : number[][];

        function doInSequence(arr, idx) {
          timeoutId = setTimeout(() => {
            observer.next(arr[idx]);
            if (idx === arr.length - 1) {
              observer.complete();
            } else {
              doInSequence(arr, ++idx);
            }
          }, 1000);
        }
      
        doInSequence(tiles, 0);
      
        // Unsubscribe should clear the timeout to stop execution
        return {
          unsubscribe() {
            clearTimeout(timeoutId);
          }
        };
    }
}