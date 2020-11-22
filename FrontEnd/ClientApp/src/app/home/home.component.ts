import { Component } from '@angular/core';
import { FormGroup, FormBuilder, FormArray, FormControl } from '@angular/forms';
import { SolverService } from '../services/solver.service';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  public currentTiles: Array<number> = new Array<number>();
  public errorMessage: string;
  form: FormGroup;
  sizes = new FormControl();
  sizeList: number[] = [2, 3, 4, 5];

  constructor( private solverService:SolverService,
    private fb: FormBuilder) {
    this.form = this.fb.group({
      published: true,
      tiles: this.fb.array([]),
    });

  }
  addTiles() {
    const tiles = this.form.controls.tiles as FormArray;
    const size = this.sizes.value * this.sizes.value;
    this.currentTiles = new Array<number>();
    tiles.clear();
    for (var i = 0; i < size; i++)
    {
      tiles.push(this.fb.group({
        tile: 0,
      }));
    }
  }
  log(){
    const tiles = this.form.controls.tiles as FormArray;
    let array = new Array();
    for (let i = 0; i < tiles.length; i++) {
      let tileValue = tiles.at(i).get('tile').value;
      array.push(tileValue);
    }
    console.log(array);
  }
  solve(){
    const tiles = this.form.controls.tiles as FormArray;
    let array = new Array<number>();
    for (let i = 0; i < tiles.length; i++) {
      let tileValue: number = +tiles.at(i).get('tile').value;
      array.push(tileValue);
    }
    this.solverService
      .solveGame(array)
      .subscribe(
        data => {
          if (data.fields.length==0){
            this.errorMessage = "The puzzle could not be solved";
          }
          else{
            this.errorMessage = "";
          }
          for (let i = 0; i < data.fields.length; i++)
          {
            setTimeout( () => {
              this.currentTiles = data.fields[i];
              console.log(this.currentTiles);
            }, 1000*i
            );
          }
        },
        error =>{
          this.errorMessage = error;
        },
        () =>{
          console.log(this.currentTiles);
        }
      )
  }
  trackByFn(index: any, item: any) {
    return index;
 }
}
