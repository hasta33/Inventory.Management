import {Directive, HostListener, Input} from '@angular/core';
import {Table} from "primeng/table";

@Directive({
  selector: '[pAddRow]'
})
export class AddRowDirective {
  @Input() table: Table | any;
  @Input() newRow: any;

  @HostListener('click', ['$event'])
  onClick(event: Event) {

    // Insert a new row
    this.table.value.push(this.newRow);
    // Set the new row in edit mode
    this.table.initRowEdit(this.newRow);

    event.preventDefault();
  }
}



/*
* @Directive({
  selector: '[pAddRow]'
})
export class AddRowDirective {
  @Input() table: Table | any;
  @Input() newRow: any;

  @HostListener('click', ['$event'])
  onClick(event: Event) {

    // Insert a new row
    this.table.value.push(this.newRow);
    // Set the new row in edit mode
    this.table.initRowEdit(this.newRow);

    event.preventDefault();
  }
}

**/
